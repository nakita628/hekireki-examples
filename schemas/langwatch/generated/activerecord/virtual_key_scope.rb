class VirtualKeyScope < ApplicationRecord
  self.table_name = "VirtualKeyScope"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt

  enum :scopeType, { organization: "ORGANIZATION", team: "TEAM", project: "PROJECT" }, validate: true

  validates :scopeId, presence: true, uniqueness: { scope: [ :virtualKeyId, :scopeType ] }

  belongs_to :virtual_key, foreign_key: "virtualKeyId", inverse_of: :scopes
end
