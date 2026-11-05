class ScimManagedConnection < ApplicationRecord
  self.table_name = "scimManagedConnection"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :creationRequestId, presence: true, uniqueness: true
  validates :creationRequestId, uniqueness: true
  validates :connectionId, presence: true, uniqueness: true
  validates :connectionId, uniqueness: true
  validates :provisioningDomainId, presence: true
  validates :status, presence: true
  validates :revision, presence: true
  validates :createdBy, presence: true

  has_many :scimmanagedcredentials, class_name: "ScimManagedCredential", foreign_key: "connectionRecordId",
                                    inverse_of: :scimmanagedconnection, dependent: :destroy
  has_many :scimmanagedconnectionevents, class_name: "ScimManagedConnectionEvent", foreign_key: "connectionRecordId",
                                         inverse_of: :scimmanagedconnection, dependent: :destroy
end
