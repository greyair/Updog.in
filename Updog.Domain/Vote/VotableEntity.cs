using System;

namespace Updog.Domain {
    /// <summary>
    /// A resource that can be voted. on.
    /// </summary>
    public abstract class VotableEntity : IUserEntity {
        #region Properties
        /// <summary>
        /// The unique ID of the entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The user it belongs to.
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// The vote resource type it is.
        /// </summary>
        public abstract VotableEntityType EntityType { get; }

        /// <summary>
        /// How many upvotes the resource has recieved.
        /// </summary>
        public int Upvotes { get; set; }

        /// <summary>
        /// How many downvotes the resource has recieved.
        /// </summary>
        public int Downvotes { get; set; }

        /// <summary>
        /// The vote state for the user viewing the entity.
        /// </summary>
        public Vote? Vote { get; set; } = null!;
        #endregion

        #region Publics
        /// <summary>
        /// Add a new vote to the counts.
        /// </summary>
        /// <param name="vote">The type of vote to add.</param>
        public void AddVote(VoteDirection vote) {
            switch (vote) {
                case VoteDirection.Up:
                    Upvotes++;
                    break;
                case VoteDirection.Down:
                    Downvotes++;
                    break;
            }

            switch (EntityType) {
                case VotableEntityType.Comment:
                    User.CommentKarma += (int)vote;
                    break;
                case VotableEntityType.Post:
                    User.PostKarma += (int)vote;
                    break;
            };
        }

        /// <summary>
        /// Remove a vote from the couns.
        /// </summary>
        /// <param name="vote">The vote to remove.</param>
        public void RemoveVote(VoteDirection vote) {
            switch (vote) {
                case VoteDirection.Up:
                    Upvotes--;
                    break;
                case VoteDirection.Down:
                    Downvotes--;
                    break;
            }

            switch (EntityType) {
                case VotableEntityType.Comment:
                    User.CommentKarma -= (int)vote;
                    break;
                case VotableEntityType.Post:
                    User.PostKarma -= (int)vote;
                    break;
            };
        }
        #endregion
    }
}