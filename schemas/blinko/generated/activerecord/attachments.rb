class Attachments < ApplicationRecord
  self.inheritance_column = nil

  attribute :isShare, default: false
  attribute :sharePassword, default: ""
  attribute :name, default: ""
  attribute :path, default: ""
  attribute :size, default: BigDecimal("0")
  attribute :type, default: ""
  attribute :sortOrder, default: 0
  attribute :perfixPath, default: ""
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  belongs_to :note, class_name: "Notes", foreign_key: "noteId", optional: true, inverse_of: :attachments
  belongs_to :account, class_name: "Accounts", foreign_key: "accountId", optional: true, inverse_of: :attachments
end
