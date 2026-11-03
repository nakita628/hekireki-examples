class Recipient < ApplicationRecord
  self.table_name = "Recipient"

  attribute :name, default: ""
  attribute :reminderCount, default: 0

  enum :role, { cc: "CC", signer: "SIGNER", viewer: "VIEWER", approver: "APPROVER", assistant: "ASSISTANT" },
       default: :signer, validate: true
  enum :readStatus, { not_opened: "NOT_OPENED", opened: "OPENED" }, default: :not_opened, validate: true
  enum :signingStatus, { not_signed: "NOT_SIGNED", signed: "SIGNED", rejected: "REJECTED" }, default: :not_signed,
                                                                                             validate: true
  enum :sendStatus, { not_sent: "NOT_SENT", sent: "SENT" }, default: :not_sent, validate: true

  validates :email, presence: true, length: { maximum: 255 }
  validates :name, length: { maximum: 255 }
  validates :token, presence: true

  belongs_to :envelope, foreign_key: "envelopeId", inverse_of: :recipients
  has_one :csc_credential, foreign_key: "recipientId", inverse_of: :recipient, dependent: :destroy
  has_one :csc_session, foreign_key: "recipientId", inverse_of: :recipient, dependent: :destroy
  has_many :fields, foreign_key: "recipientId", inverse_of: :recipient, dependent: :destroy
  has_many :signatures, foreign_key: "recipientId", inverse_of: :recipient, dependent: :destroy
end
