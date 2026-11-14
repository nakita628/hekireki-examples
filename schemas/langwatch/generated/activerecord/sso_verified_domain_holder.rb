class SsoVerifiedDomainHolder < ApplicationRecord
  self.table_name = "SsoVerifiedDomainHolder"
  self.primary_key = %w[domain connectionId]

  belongs_to :ownership, class_name: "SsoVerifiedDomain", foreign_key: [ :domain, :organizationId ],
                         primary_key: [ :domain, :organizationId ], inverse_of: :holders
end
