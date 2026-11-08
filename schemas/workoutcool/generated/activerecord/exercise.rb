class Exercise < ApplicationRecord
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :slug, uniqueness: { allow_nil: true }
  validates :slugEn, uniqueness: { allow_nil: true }

  has_many :exercise_attributes, foreign_key: "exerciseId", inverse_of: :exercise, dependent: :destroy
  has_many :workout_session_exercise, foreign_key: "exerciseId", inverse_of: :exercise, dependent: :restrict_with_error
  has_many :program_session_exercise, foreign_key: "exerciseId", inverse_of: :exercise, dependent: :restrict_with_error
  has_many :favorites_by_users, class_name: "UserFavoriteExercise", foreign_key: "exerciseId", inverse_of: :exercise,
                                dependent: :destroy
end
