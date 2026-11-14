class ModelProviderScope < ApplicationRecord
  self.table_name = "ModelProviderScope"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt

  enum :scopeType, { organization: "ORGANIZATION", team: "TEAM", project: "PROJECT" }, validate: true

  validates :scopeId, presence: true, uniqueness: { scope: [ :modelProviderId, :scopeType ] }

  belongs_to :model_provider, foreign_key: "modelProviderId", inverse_of: :scopes
end
