using System.Numerics;
using AltV.Atlas.Drawing.Client.Models;
using AltV.Net.Client;
using AltV.Net.Data;
namespace AltV.Atlas.Drawing.Client;

public static class AtlasDrawing
{
    //JS version from splak converted to c#
    public static void DrawBoundingBox( Vector3 start, Vector3 end, uint entity, Rgba? color = null )
    {
        color ??= new Rgba( 255, 0, 0, 255 );

        var lines = new List<Line>
        {
            // 3D Diagonal
            new( start, end ),
            new( new Vector3( start.X, start.Y, end.Z ), new Vector3( end.X, end.Y, start.Z ) ),
            new( new Vector3( end.X, start.Y, end.Z ), new Vector3( start.X, end.Y, start.Z ) ),
            new( new Vector3( end.X, start.Y, start.Z ), new Vector3( start.X, end.Y, end.Z ) ),

            // Front Area
            new( start, new Vector3( start.X, start.Y, end.Z ) ),
            new( new Vector3( start.X, start.Y, end.Z ), new Vector3( end.X, start.Y, end.Z ) ),
            new( new Vector3( end.X, start.Y, end.Z ), new Vector3( end.X, start.Y, start.Z ) ),
            new( new Vector3( end.X, start.Y, start.Z ), start ),
            new( start, new Vector3( end.X, start.Y, end.Z ) ),
            new( new Vector3( end.X, start.Y, start.Z ), new Vector3( start.X, start.Y, end.Z ) ),

            // Rear Area
            new( end, new Vector3( end.X, end.Y, start.Z ) ),
            new( new Vector3( end.X, end.Y, start.Z ), new Vector3( start.X, end.Y, start.Z ) ),
            new( new Vector3( start.X, end.Y, start.Z ), new Vector3( start.X, end.Y, end.Z ) ),
            new( new Vector3( start.X, end.Y, end.Z ), end ),
            new( end, new Vector3( start.X, end.Y, start.Z ) ),
            new( new Vector3( start.X, end.Y, end.Z ), new Vector3( end.X, end.Y, start.Z ) ),

            // Left Area
            new( start, new Vector3( start.X, end.Y, start.Z ) ),
            new( new Vector3( start.X, start.Y, end.Z ), new Vector3( start.X, end.Y, end.Z ) ),
            new( start, new Vector3( start.X, end.Y, start.Z ) ),
            new( new Vector3( start.X, start.Y, end.Z ), new Vector3( start.X, start.Y, end.Z ) ),

            // Right Area
            new( end, new Vector3( end.X, start.Y, end.Z ) ),
            new( new Vector3( end.X, end.Y, start.Z ), new Vector3( end.X, start.Y, start.Z ) ),
            new( end, new Vector3( end.X, start.Y, end.Z ) ),
            new( new Vector3( end.X, end.Y, start.Z ), new Vector3( end.X, end.Y, end.Z ) ),

            // Top Area
            new( end, new Vector3( start.X, start.Y, end.Z ) ),
            new( start, new Vector3( end.X, end.Y, end.Z ) ),

            // Bottom Area
            new( start, new Vector3( end.X, end.Y, start.Z ) ),
            new( end, new Vector3( start.X, start.Y, start.Z ) ),
        };

        foreach( var line in lines )
        {
            var lineStart = Alt.Natives.GetOffsetFromEntityInWorldCoords( entity, line.Start.X, line.Start.Y, line.Start.Z );
            var lineEnd = Alt.Natives.GetOffsetFromEntityInWorldCoords( entity, line.End.X, line.End.Y, line.End.Z );

            Alt.Natives.DrawLine( lineStart.X, lineStart.Y, lineStart.Z, lineEnd.X, lineEnd.Y, lineEnd.Z, color.Value.R, color.Value.G, color.Value.B, color.Value.A );
        }
    }
}