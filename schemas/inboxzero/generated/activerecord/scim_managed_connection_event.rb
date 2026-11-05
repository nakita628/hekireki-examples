class ScimManagedConnectionEvent < ApplicationRecord
  self.table_name = "scimManagedConnectionEvent"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :eventKey, presence: true, uniqueness: true
  validates :eventKey, uniqueness: true
  validates :sequence, presence: true
  validates :type, presence: true
  validates :actorId, presence: true

  belongs_to :scimmanagedconnection, class_name: "ScimManagedConnection", foreign_key: "connectionRecordId",
                                     inverse_of: :scimmanagedconnectionevents
end
