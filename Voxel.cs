using System.Numerics; // For using Vector3

namespace VoxelStoragePerformanceComparison
{
    public class Voxel
    {
        // Position, Rotation, Scale
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }

        // Material Properties
        public byte MaterialType { get; set; }
        public float Roughness { get; set; }
        public float Metalness { get; set; }

        // Density and Mass
        public byte Density { get; set; }
        public float Mass { get; set; }

        // Physics Properties
        public bool IsStatic { get; set; }
        public bool IsCollidable { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector3 Acceleration { get; set; }

        // Visual Properties
        public string Color { get; set; } // Example: "#FF5733"
        public string TexturePath { get; set; }

        // Lighting Data
        public float Emission { get; set; }
        public float Opacity { get; set; }

        // Health/Durability
        public int Durability { get; set; }
        public bool IsDestructible { get; set; }

        // Environmental Interaction
        public bool IsAffectedByGravity { get; set; }
        public bool IsFlammable { get; set; }

        // Constructor to Initialize All Properties
        public Voxel(Vector3 position, Vector3 rotation, Vector3 scale, byte materialType, float roughness,
                     float metalness, byte density, float mass, bool isStatic, bool isCollidable,
                     Vector3 velocity, Vector3 acceleration, string color, string texturePath,
                     float emission, float opacity, int durability, bool isDestructible,
                     bool isAffectedByGravity, bool isFlammable)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
            MaterialType = materialType;
            Roughness = roughness;
            Metalness = metalness;
            Density = density;
            Mass = mass;
            IsStatic = isStatic;
            IsCollidable = isCollidable;
            Velocity = velocity;
            Acceleration = acceleration;
            Color = color;
            TexturePath = texturePath;
            Emission = emission;
            Opacity = opacity;
            Durability = durability;
            IsDestructible = isDestructible;
            IsAffectedByGravity = isAffectedByGravity;
            IsFlammable = isFlammable;
        }

        public override string ToString()
        {
            return $"Voxel Details:\n" +
                   $"  Position:        ({Position.X}, {Position.Y}, {Position.Z})\n" +
                   $"  Rotation:        ({Rotation.X}, {Rotation.Y}, {Rotation.Z})\n" +
                   $"  Scale:           ({Scale.X}, {Scale.Y}, {Scale.Z})\n" +
                   $"  Material Type:   {MaterialType}\n" +
                   $"  Roughness:       {Roughness}\n" +
                   $"  Metalness:       {Metalness}\n" +
                   $"  Density:         {Density}\n" +
                   $"  Mass:            {Mass}\n" +
                   $"  Is Static:       {IsStatic}\n" +
                   $"  Is Collidable:   {IsCollidable}\n" +
                   $"  Velocity:        ({Velocity.X}, {Velocity.Y}, {Velocity.Z})\n" +
                   $"  Acceleration:    ({Acceleration.X}, {Acceleration.Y}, {Acceleration.Z})\n" +
                   $"  Color:           {Color}\n" +
                   $"  Texture Path:    {TexturePath}\n" +
                   $"  Emission:        {Emission}\n" +
                   $"  Opacity:         {Opacity}\n" +
                   $"  Durability:      {Durability}\n" +
                   $"  Is Destructible: {IsDestructible}\n" +
                   $"  Affected by Gravity: {IsAffectedByGravity}\n" +
                   $"  Is Flammable:    {IsFlammable}";
        }

        public bool Equals(Voxel other)
        {
            if (other == null)
                return false;

            // Compare each property of the voxel
            return Position.Equals(other.Position) &&
                   Rotation.Equals(other.Rotation) &&
                   Scale.Equals(other.Scale) &&
                   MaterialType == other.MaterialType &&
                   Roughness == other.Roughness &&
                   Metalness == other.Metalness &&
                   Density == other.Density &&
                   Mass == other.Mass &&
                   IsStatic == other.IsStatic &&
                   IsCollidable == other.IsCollidable &&
                   Velocity.Equals(other.Velocity) &&
                   Acceleration.Equals(other.Acceleration) &&
                   Color == other.Color &&
                   TexturePath == other.TexturePath &&
                   Emission == other.Emission &&
                   Opacity == other.Opacity &&
                   Durability == other.Durability &&
                   IsDestructible == other.IsDestructible &&
                   IsAffectedByGravity == other.IsAffectedByGravity &&
                   IsFlammable == other.IsFlammable;
        }
    }
}
