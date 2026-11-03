class EmailTransport < ApplicationRecord
  self.table_name = "EmailTransport"
  self.inheritance_column = nil

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { smtp_auth: "SMTP_AUTH", smtp_api: "SMTP_API", resend: "RESEND", mailchannels: "MAILCHANNELS" },
       validate: true

  validates :name, presence: true
  validates :fromName, presence: true
  validates :fromAddress, presence: true
  validates :config, presence: true

  has_many :subscription_claims, foreign_key: "emailTransportId", inverse_of: :email_transport, dependent: :nullify
  has_many :organisation_claims, foreign_key: "emailTransportId", inverse_of: :email_transport, dependent: :nullify
end
