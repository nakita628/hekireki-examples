class DeletionCleanup < ApplicationRecord
  self.table_name = "DeletionCleanup"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid2.call }
  attribute :tenantIds, default: -> { [] }
  attribute :responseIds, default: -> { [] }
  attribute :fileKeys, default: -> { [] }
  attribute :attempts, default: 0

  enum :kind, { hub_survey: "hubSurvey", hub_responses: "hubResponses", storage_files: "storageFiles", storage_survey_folder: "storageSurveyFolder" },
       validate: true

  validates :organizationId, presence: true
  validates :workspaceId, presence: true
  validates :surveyId, presence: true
end
