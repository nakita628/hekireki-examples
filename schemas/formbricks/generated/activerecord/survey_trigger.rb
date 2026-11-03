class SurveyTrigger < ApplicationRecord
  self.table_name = "SurveyTrigger"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :surveyId, uniqueness: { scope: :actionClassId }

  belongs_to :survey, foreign_key: "surveyId", inverse_of: :triggers
  belongs_to :action_class, foreign_key: "actionClassId", inverse_of: :survey_triggers
end
