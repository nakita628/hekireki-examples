class WorkoutSession < ApplicationRecord
  attribute :id, default: -> { Cuid.generate }

  validates :startedAt, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :workout_session
  has_one :user_session_progress, foreign_key: "workoutSessionId", inverse_of: :workout_session, dependent: :nullify
  has_many :exercises, class_name: "WorkoutSessionExercise", foreign_key: "workoutSessionId",
                       inverse_of: :workout_session, dependent: :destroy
end
