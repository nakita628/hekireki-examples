class WorkoutSessionExercise < ApplicationRecord
  attribute :id, default: -> { Cuid.generate }

  validates :order, presence: true

  belongs_to :workout_session, foreign_key: "workoutSessionId", inverse_of: :exercises
  belongs_to :exercise, foreign_key: "exerciseId", inverse_of: :workout_session_exercise
  has_many :sets, class_name: "WorkoutSet", foreign_key: "workoutSessionExerciseId",
                  inverse_of: :workout_session_exercise, dependent: :destroy
end
