class OAuthApp < ApplicationRecord
  self.table_name = "oauth_app"

  attribute :id, default: -> { Cuid.generate }
  attribute :allow_device_flow, default: false
  attribute :dynamic_registration, default: false
  alias_attribute :updated_at, :last_modified_time

  validates :name, presence: true
  validates :homepage, presence: true
  validates :client_id, presence: true, uniqueness: true
  validates :created_by, presence: true

  has_many :secrets, class_name: "OAuthAppSecret", foreign_key: "client_id", primary_key: "client_id",
                     inverse_of: :o_auth_app, dependent: :destroy
  has_many :authorized, class_name: "OAuthAppAuthorized", foreign_key: "client_id", primary_key: "client_id",
                        inverse_of: :o_auth_app, dependent: :destroy
  has_many :tokens, class_name: "OAuthAppToken", foreign_key: "client_id", primary_key: "client_id",
                    inverse_of: :o_auth_app, dependent: :destroy
end
