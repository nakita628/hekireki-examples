class WorkoutSet < ApplicationRecord
  self.inheritance_column = nil

  attribute :id, default: -> { Cuid.generate }
  attribute :valuesInt, default: -> { [] }
  attribute :valuesSec, default: -> { [] }
  attribute :completed, default: false

  enum :type, { time: "TIME", weight: "WEIGHT", reps: "REPS", bodyweight: "BODYWEIGHT", na: "NA" }, validate: true

  validates :setIndex, presence: true

  belongs_to :workout_session_exercise, foreign_key: "workoutSessionExerciseId", inverse_of: :sets
end
