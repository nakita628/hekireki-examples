class ConfigurationObjectLink < ApplicationRecord
  self.table_name = "ConfigurationObjectLink"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :deleted, default: false
  attribute :type, default: "push"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :configuration_object_link
  belongs_to :from, class_name: "ConfigurationObject", foreign_key: "fromId", inverse_of: :to_links
  belongs_to :to, class_name: "ConfigurationObject", foreign_key: "toId", inverse_of: :from_links
end
