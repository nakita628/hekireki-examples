class SurveyEmbeddedData < ApplicationRecord
  self.table_name = "SurveyEmbeddedData"

  attribute :id, default: -> { Cuid.generate }

  validates :surveyId, uniqueness: { scope: :embeddedDataId }
  validates :storageKey, presence: true, uniqueness: { scope: :surveyId }
  validates :order, presence: true

  belongs_to :survey, foreign_key: [ :surveyId, :workspaceId ], primary_key: [ :id, :workspaceId ],
                      inverse_of: :embedded_data_links
  belongs_to :embedded_data, foreign_key: [ :embeddedDataId, :workspaceId ], primary_key: [ :id, :workspaceId ],
                             inverse_of: :survey_links
end
