class ModelDefaultConfigScope < ApplicationRecord
  self.table_name = "ModelDefaultConfigScope"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt

  enum :scopeType, { organization: "ORGANIZATION", team: "TEAM", project: "PROJECT" }, validate: true

  validates :scopeId, presence: true, uniqueness: { scope: [ :configId, :scopeType ] }

  belongs_to :config, class_name: "ModelDefaultConfig", foreign_key: "configId", inverse_of: :scopes
end
