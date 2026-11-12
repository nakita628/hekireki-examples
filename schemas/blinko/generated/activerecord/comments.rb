class Comments < ApplicationRecord
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :content, presence: true

  belongs_to :note, class_name: "Notes", foreign_key: "noteId", inverse_of: :comments
  belongs_to :account, class_name: "Accounts", foreign_key: "accountId", optional: true, inverse_of: :comments
  belongs_to :parent, class_name: "Comments", foreign_key: "parentId", optional: true, inverse_of: :replies
  has_many :replies, class_name: "Comments", foreign_key: "parentId", inverse_of: :parent, dependent: :nullify
end
