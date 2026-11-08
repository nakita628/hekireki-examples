class UserFavoriteExercise < ApplicationRecord
  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :updatedAt

  validates :userId, uniqueness: { scope: :exerciseId }

  belongs_to :user, foreign_key: "userId", inverse_of: :favorite_exercises
  belongs_to :exercise, foreign_key: "exerciseId", inverse_of: :favorites_by_users
end
