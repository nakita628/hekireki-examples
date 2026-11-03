class Agreement < ApplicationRecord
  self.table_name = "Agreement"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :contentType, default: "LINK"
  attribute :signingProvider, default: "LEGACY"
  attribute :requireName, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :content, presence: true
  validates :signingExternalId, uniqueness: { allow_nil: true }

  belongs_to :team, foreign_key: "teamId", inverse_of: :agreements
  has_many :links, foreign_key: "agreementId", inverse_of: :agreement, dependent: :nullify
  has_many :responses, class_name: "AgreementResponse", foreign_key: "agreementId", inverse_of: :agreement,
                       dependent: :destroy
end
