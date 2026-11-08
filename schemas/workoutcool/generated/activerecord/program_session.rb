class ProgramSession < ApplicationRecord
  attribute :id, default: -> { Cuid.generate }
  attribute :isPremium, default: true

  validates :sessionNumber, presence: true, uniqueness: { scope: :weekId }
  validates :title, presence: true
  validates :titleEn, presence: true
  validates :titleEs, presence: true
  validates :titlePt, presence: true
  validates :titleRu, presence: true
  validates :titleZhCn, presence: true
  validates :slug, presence: true, uniqueness: { scope: :weekId }
  validates :slugEn, presence: true, uniqueness: { scope: :weekId }
  validates :slugEs, presence: true, uniqueness: { scope: :weekId }
  validates :slugPt, presence: true, uniqueness: { scope: :weekId }
  validates :slugRu, presence: true, uniqueness: { scope: :weekId }
  validates :slugZhCn, presence: true, uniqueness: { scope: :weekId }
  validates :description, presence: true
  validates :descriptionEn, presence: true
  validates :descriptionEs, presence: true
  validates :descriptionPt, presence: true
  validates :descriptionRu, presence: true
  validates :descriptionZhCn, presence: true
  validates :estimatedMinutes, presence: true

  belongs_to :week, class_name: "ProgramWeek", foreign_key: "weekId", inverse_of: :sessions
  has_many :exercises, class_name: "ProgramSessionExercise", foreign_key: "sessionId", inverse_of: :session,
                       dependent: :destroy
  has_many :user_progress, class_name: "UserSessionProgress", foreign_key: "sessionId", inverse_of: :session,
                           dependent: :restrict_with_error
end
