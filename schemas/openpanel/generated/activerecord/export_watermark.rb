class ExportWatermark < ApplicationRecord
  attribute :lastEventId, default: ""
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :projectId, uniqueness: { scope: :integrationId }
  validates :lastInsertedAt, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :export_watermarks
  belongs_to :integration, foreign_key: "integrationId", inverse_of: :export_watermarks
end
