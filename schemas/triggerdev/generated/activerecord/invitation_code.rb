class InvitationCode < ApplicationRecord
  self.table_name = "InvitationCode"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :code, presence: true, uniqueness: true

  has_many :users, foreign_key: "invitationCodeId", inverse_of: :invitation_code, dependent: :nullify
end
