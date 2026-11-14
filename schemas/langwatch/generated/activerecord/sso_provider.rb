class SsoProvider < ApplicationRecord
  self.table_name = "SsoProvider"

  validates :issuer, presence: true
  validates :providerId, presence: true, uniqueness: true
  validates :domain, presence: true
end
