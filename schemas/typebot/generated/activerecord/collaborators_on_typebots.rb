class CollaboratorsOnTypebots < ApplicationRecord
  self.table_name = "CollaboratorsOnTypebots"
  self.inheritance_column = nil

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { read: "READ", write: "WRITE", full_access: "FULL_ACCESS" }, validate: true

  validates :userId, uniqueness: { scope: :typebotId }

  belongs_to :typebot, foreign_key: "typebotId", inverse_of: :collaborators
  belongs_to :user, foreign_key: "userId", inverse_of: :collaborators_on_typebots
end
