using UnityEngine;

namespace Scripts.Movement {
    public static class CollisionDetection {
        public static Vector2 CheckCollision(BoxCollider2D collider, Vector2 velocity, LayerMask layerMask) {
            if (velocity.x != 0) {
                // calculate edges
                var max = collider.bounds.max;
                var min = collider.bounds.min;
                var xVal = velocity.x > 0 ? max.x + .05f : min.x - .05f;
                var midY = max.y - ((max.y - min.y) / 2);
                var edges = new Vector2[] {
                    new Vector2(xVal, max.y),
                    new Vector2(xVal, midY),
                    new Vector2(xVal, min.y),
                };

                // cast rays
                var direction = velocity.x > 0 ? Vector2.right : Vector2.left;
                var speed = Mathf.Abs(velocity.x);
                var closest = speed;
                foreach (var edge in edges) {
                    var hit = Physics2D.Raycast(edge, direction, speed, layerMask);
                    if (hit.collider != null) {
                        var distance = Mathf.Abs((hit.point - edge).x);
                        if (distance < closest) {
                            closest = distance;
                        }
                    }
                }

                if (closest < speed)
                    velocity.x = (closest * direction.x);
            }
            if (velocity.y != 0) {
                // calculate edges
                var max = collider.bounds.max;
                var min = collider.bounds.min;
                var yVal = velocity.y > 0 ? max.y + .05f : min.y - .05f;
                var midX = max.x - ((max.x - min.x) / 2);
                var edges = new Vector2[] {
                    new Vector2(max.x, yVal),
                    new Vector2(midX, yVal),
                    new Vector2(min.x, yVal),
                };

                // cast rays
                var direction = velocity.y > 0 ? Vector2.up : Vector2.down;
                var speed = Mathf.Abs(velocity.y);
                var closest = speed;
                foreach (var edge in edges) {
                    var hit = Physics2D.Raycast(edge, direction, speed, layerMask);
                    if (hit.collider != null) {
                        var distance = Mathf.Abs((hit.point - edge).y);
                        if (distance < closest) {
                            closest = distance;
                        }
                    }
                }

                if (closest < speed)
                    velocity.y = (closest * direction.y);
            }

            return velocity;
        }
    }
}
