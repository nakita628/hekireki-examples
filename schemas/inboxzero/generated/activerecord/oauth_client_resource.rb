class OauthClientResource < ApplicationRecord
  self.table_name = "OauthClientResource"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :clientId, uniqueness: { scope: :resourceId }

  belongs_to :client, class_name: "OauthClient", foreign_key: "clientId", primary_key: "clientId",
                      inverse_of: :oauth_client_resources
  belongs_to :oauth_resource, foreign_key: "resourceId", primary_key: "identifier", inverse_of: :oauth_client_resources
end
