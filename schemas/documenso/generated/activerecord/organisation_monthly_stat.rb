class OrganisationMonthlyStat < ApplicationRecord
  self.table_name = "OrganisationMonthlyStat"

  attribute :documentCount, default: 0
  attribute :emailCount, default: 0
  attribute :apiCount, default: 0
  attribute :emailReports, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :period, presence: true, uniqueness: { scope: :organisationId }

  belongs_to :organisation, foreign_key: "organisationId", inverse_of: :monthly_stats
end
