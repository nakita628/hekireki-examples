class ScimManagedCredential < ApplicationRecord
  self.table_name = "scimManagedCredential"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :credentialId, presence: true, uniqueness: true
  validates :credentialId, uniqueness: true
  validates :tokenDigest, presence: true
  validates :hashVersion, presence: true
  validates :activeSlotKey, presence: true, uniqueness: true
  validates :activeSlotKey, uniqueness: true
  validates :status, presence: true
  validates :serializedScopes, presence: true
  validates :expiresAt, presence: true
  validates :createdBy, presence: true

  belongs_to :scimmanagedconnection, class_name: "ScimManagedConnection", foreign_key: "connectionRecordId",
                                     inverse_of: :scimmanagedcredentials
end
