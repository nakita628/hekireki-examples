class MetricsDashboard < ApplicationRecord
  self.table_name = "MetricsDashboard"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :description, default: ""
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :friendlyId, presence: true, uniqueness: true
  validates :title, presence: true
  validates :layout, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :metrics_dashboards
  belongs_to :project, foreign_key: "projectId", optional: true, inverse_of: :metrics_dashboards
  belongs_to :owner, class_name: "User", foreign_key: "ownerId", optional: true, inverse_of: :metrics_dashboards
end
