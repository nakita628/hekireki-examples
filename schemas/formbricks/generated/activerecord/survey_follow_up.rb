class SurveyFollowUp < ApplicationRecord
  self.table_name = "SurveyFollowUp"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :name, presence: true
  validates :trigger, exclusion: { in: [ nil ] }
  validates :action, exclusion: { in: [ nil ] }

  belongs_to :survey, foreign_key: "surveyId", inverse_of: :follow_ups
end
