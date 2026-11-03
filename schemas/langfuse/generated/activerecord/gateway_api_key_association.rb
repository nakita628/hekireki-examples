class GatewayApiKeyAssociation < ApplicationRecord
  self.primary_key = "api_key_id"

  attribute :metadata, default: -> { JSON.parse("{}") }

  belongs_to :api_key, inverse_of: :gateway_association
end
