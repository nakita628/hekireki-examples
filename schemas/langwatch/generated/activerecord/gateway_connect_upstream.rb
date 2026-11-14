class GatewayConnectUpstream < ApplicationRecord
  self.table_name = "GatewayConnectUpstream"
  self.primary_key = "organizationId"

  alias_attribute :updated_at, :updatedAt

  validates :baseUrl, presence: true
  validates :encryptedToken, presence: true
  validates :instanceId, presence: true
end
