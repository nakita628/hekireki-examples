class UserProgramEnrollment < ApplicationRecord
  attribute :id, default: -> { Cuid.generate }
  attribute :currentWeek, default: 1
  attribute :currentSession, default: 1
  attribute :completedSessions, default: 0
  attribute :isActive, default: true

  validates :userId, uniqueness: { scope: :programId }

  belongs_to :user, foreign_key: "userId", inverse_of: :program_enrollments
  belongs_to :program, foreign_key: "programId", inverse_of: :enrollments
  has_many :session_progress, class_name: "UserSessionProgress", foreign_key: "enrollmentId", inverse_of: :enrollment,
                              dependent: :destroy
end
