class CommentReaction < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :emoji, presence: true, uniqueness: { scope: %i[comment_id user_id] }

  belongs_to :project, inverse_of: :comment_reactions
  belongs_to :comment, inverse_of: :reactions
  belongs_to :user, inverse_of: :comment_reactions
end
