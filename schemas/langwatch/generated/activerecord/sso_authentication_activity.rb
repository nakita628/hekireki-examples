class SsoAuthenticationActivity < ApplicationRecord
  self.table_name = "SsoAuthenticationActivity"

  validates :organizationId, presence: true
  validates :connectionId, presence: true
  validates :userId, presence: true
  validates :authenticatedAt, presence: true
end
