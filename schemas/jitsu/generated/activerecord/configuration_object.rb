class ConfigurationObject < ApplicationRecord
  self.table_name = "ConfigurationObject"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :deleted, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :type, presence: true

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :configuration_object
  has_many :from_links, class_name: "ConfigurationObjectLink", foreign_key: "toId", inverse_of: :to,
                        dependent: :restrict_with_error
  has_many :to_links, class_name: "ConfigurationObjectLink", foreign_key: "fromId", inverse_of: :from,
                      dependent: :restrict_with_error
  has_many :profile_builder, foreign_key: "destinationId", inverse_of: :destination, dependent: :nullify
  has_many :profile_builder_function, foreign_key: "functionId", inverse_of: :function, dependent: :restrict_with_error
end
