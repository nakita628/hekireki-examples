class DashboardsV2 < ApplicationRecord
  self.table_name = "DashboardsV2"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :scope, { private: "PRIVATE", project: "PROJECT", organization: "ORGANIZATION" }, default: :project, prefix: true,
                                                                                         validate: true

  validates :projectId, presence: true
  validates :name, presence: true
end
