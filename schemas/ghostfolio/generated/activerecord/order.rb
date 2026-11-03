class Order < ApplicationRecord
  self.table_name = "Order"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { buy: "BUY", dividend: "DIVIDEND", fee: "FEE", interest: "INTEREST", liability: "LIABILITY", sell: "SELL" },
       validate: true

  validates :date, presence: true
  validates :fee, presence: true
  validates :quantity, presence: true
  validates :unitPrice, presence: true

  belongs_to :account, foreign_key: [ :accountId, :accountUserId ], optional: true, inverse_of: :activities
  belongs_to :user, foreign_key: "userId", inverse_of: :activities
  belongs_to :symbol_profile, foreign_key: "symbolProfileId", inverse_of: :activities
  has_and_belongs_to_many :tags, join_table: "_OrderToTag", foreign_key: "A", association_foreign_key: "B"
end
