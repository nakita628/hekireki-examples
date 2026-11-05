class Client < ApplicationRecord
  self.inheritance_column = nil

  attribute :ignoreCorsAndSecret, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { read: "read", write: "write", root: "root" }, default: :write, validate: true

  validates :name, presence: true

  belongs_to :project, foreign_key: "projectId", optional: true, inverse_of: :clients
  belongs_to :organization, foreign_key: "organizationId", inverse_of: :client
end
