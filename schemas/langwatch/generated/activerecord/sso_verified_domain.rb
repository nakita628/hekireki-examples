class SsoVerifiedDomain < ApplicationRecord
  self.table_name = "SsoVerifiedDomain"
  self.primary_key = "domain"

  validates :domain, uniqueness: { scope: :organizationId }
  validates :organizationId, presence: true

  has_many :holders, class_name: "SsoVerifiedDomainHolder", foreign_key: [ :domain, :organizationId ],
                     primary_key: [ :domain, :organizationId ], inverse_of: :ownership, dependent: :destroy
end
