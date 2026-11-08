class UserSessionProgress < ApplicationRecord
  self.table_name = "user_session_progress"

  attribute :id, default: -> { Cuid.generate }

  validates :enrollmentId, uniqueness: { scope: :sessionId }
  validates :workoutSessionId, uniqueness: { allow_nil: true }

  belongs_to :enrollment, class_name: "UserProgramEnrollment", foreign_key: "enrollmentId",
                          inverse_of: :session_progress
  belongs_to :session, class_name: "ProgramSession", foreign_key: "sessionId", inverse_of: :user_progress
  belongs_to :workout_session, foreign_key: "workoutSessionId", optional: true, inverse_of: :user_session_progress
end
