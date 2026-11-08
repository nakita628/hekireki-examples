class Envelope < ApplicationRecord
  self.table_name = "Envelope"
  self.inheritance_column = nil

  attribute :useLegacyFieldInsertion, default: false
  attribute :publicTitle, default: ""
  attribute :publicDescription, default: ""
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { document: "DOCUMENT", template: "TEMPLATE" }, validate: true
  enum :status, { draft: "DRAFT", pending: "PENDING", completed: "COMPLETED", rejected: "REJECTED", cancelled: "CANCELLED" },
       default: :draft, validate: true
  enum :source, { document: "DOCUMENT", template: "TEMPLATE", template_direct_link: "TEMPLATE_DIRECT_LINK" },
       prefix: true, validate: true
  enum :visibility, { everyone: "EVERYONE", manager_and_above: "MANAGER_AND_ABOVE", admin: "ADMIN" }, default: :everyone,
                                                                                                      validate: true
  enum :templateType, { public: "PUBLIC", private: "PRIVATE", organisation: "ORGANISATION" }, default: :private,
                                                                                              prefix: true,
                                                                                              validate: true

  validates :secondaryId, presence: true, uniqueness: true
  validates :title, presence: true
  validates :signatureLevel, presence: true
  validates :internalVersion, presence: true
  validates :documentMetaId, uniqueness: true

  belongs_to :user, foreign_key: "userId", inverse_of: :envelopes
  belongs_to :team, foreign_key: "teamId", inverse_of: :envelopes
  belongs_to :folder, foreign_key: "folderId", optional: true, inverse_of: :envelopes
  belongs_to :document_meta, foreign_key: "documentMetaId", inverse_of: :envelope
  has_one :direct_link, class_name: "TemplateDirectLink", foreign_key: "envelopeId", inverse_of: :envelope,
                        dependent: :destroy
  has_many :envelope_items, foreign_key: "envelopeId", inverse_of: :envelope, dependent: :destroy
  has_many :recipients, foreign_key: "envelopeId", inverse_of: :envelope, dependent: :destroy
  has_many :fields, foreign_key: "envelopeId", inverse_of: :envelope, dependent: :destroy
  has_many :contents, class_name: "EnvelopeContent", foreign_key: "envelopeId", inverse_of: :envelope,
                      dependent: :destroy
  has_many :share_links, class_name: "DocumentShareLink", foreign_key: "envelopeId", inverse_of: :envelope,
                         dependent: :destroy
  has_many :audit_logs, class_name: "DocumentAuditLog", foreign_key: "envelopeId", inverse_of: :envelope,
                        dependent: :nullify
  has_many :envelope_attachments, foreign_key: "envelopeId", inverse_of: :envelope, dependent: :destroy
end
