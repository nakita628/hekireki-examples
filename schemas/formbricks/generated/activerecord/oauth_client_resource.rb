class OauthClientResource < ApplicationRecord
  self.table_name = "oauthClientResource"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :clientId, uniqueness: { scope: :resourceId }

  belongs_to :client, class_name: "OauthClient", foreign_key: "clientId", primary_key: "clientId",
                      inverse_of: :resources
  belongs_to :resource, class_name: "OauthResource", foreign_key: "resourceId", primary_key: "identifier",
                        inverse_of: :clients
end
