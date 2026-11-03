class RetentionSurveyRestore < ApplicationRecord
  self.table_name = "RetentionSurveyRestore"
  self.primary_key = "surveyId"

  validates :restoredAt, presence: true

  belongs_to :survey, foreign_key: "surveyId", inverse_of: :retention_restore
end
