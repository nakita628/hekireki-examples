class ProgramWeek < ApplicationRecord
  attribute :id, default: -> { Cuid.generate }

  validates :weekNumber, presence: true, uniqueness: { scope: :programId }
  validates :title, presence: true
  validates :titleEn, presence: true
  validates :titleEs, presence: true
  validates :titlePt, presence: true
  validates :titleRu, presence: true
  validates :titleZhCn, presence: true
  validates :description, presence: true
  validates :descriptionEn, presence: true
  validates :descriptionEs, presence: true
  validates :descriptionPt, presence: true
  validates :descriptionRu, presence: true
  validates :descriptionZhCn, presence: true

  belongs_to :program, foreign_key: "programId", inverse_of: :weeks
  has_many :sessions, class_name: "ProgramSession", foreign_key: "weekId", inverse_of: :week, dependent: :destroy
end
