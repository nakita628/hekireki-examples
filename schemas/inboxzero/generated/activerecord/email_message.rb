class EmailMessage < ApplicationRecord
  self.table_name = "EmailMessage"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :threadId, presence: true, uniqueness: { scope: [ :emailAccountId, :messageId ] }
  validates :messageId, presence: true
  validates :date, presence: true
  validates :from, presence: true
  validates :fromDomain, presence: true
  validates :to, presence: true
  validates :read, inclusion: { in: [ true, false ] }
  validates :sent, inclusion: { in: [ true, false ] }
  validates :draft, inclusion: { in: [ true, false ] }
  validates :inbox, inclusion: { in: [ true, false ] }

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :email_messages
end
