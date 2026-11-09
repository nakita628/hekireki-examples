class ProfileBuilder < ApplicationRecord
  self.table_name = "ProfileBuilder"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :deleted, default: false
  attribute :version, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :intermediateStorageCredentials, exclusion: { in: [ nil ] }

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :profile_builders
  belongs_to :destination, class_name: "ConfigurationObject", foreign_key: "destinationId", optional: true,
                           inverse_of: :profile_builder
  has_many :functions, class_name: "ProfileBuilderFunction", foreign_key: "profileBuilderId",
                       inverse_of: :profile_builder, dependent: :restrict_with_error
end
