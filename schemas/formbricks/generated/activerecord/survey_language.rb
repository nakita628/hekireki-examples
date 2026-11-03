class SurveyLanguage < ApplicationRecord
  self.table_name = "SurveyLanguage"
  self.primary_key = %w[languageId surveyId]

  attribute :default, default: false
  attribute :enabled, default: true

  belongs_to :language, foreign_key: "languageId", inverse_of: :survey_languages
  belongs_to :survey, foreign_key: "surveyId", inverse_of: :languages
end
