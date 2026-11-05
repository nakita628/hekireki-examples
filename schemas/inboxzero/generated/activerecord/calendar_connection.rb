class CalendarConnection < ApplicationRecord
  self.table_name = "CalendarConnection"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isConnected, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :provider, presence: true, uniqueness: { scope: [ :emailAccountId, :email ] }
  validates :email, presence: true

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :calendar_connections
  has_many :calendars, foreign_key: "connectionId", inverse_of: :connection, dependent: :destroy
end
