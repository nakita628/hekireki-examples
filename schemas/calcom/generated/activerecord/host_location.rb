class HostLocation < ApplicationRecord
  self.table_name = "HostLocation"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :userId, uniqueness: { scope: :eventTypeId }
  validates :type, presence: true

  belongs_to :host, foreign_key: [ :userId, :eventTypeId ], inverse_of: :location
  belongs_to :credential, foreign_key: "credentialId", optional: true, inverse_of: :host_locations
end
