class Role < ApplicationRecord
  self.table_name = "Role"

  attribute :id, default: -> { SecureRandom.uuid }

  enum :role, { user: "USER", admin: "ADMIN", moderator: "MODERATOR", creator: "CREATOR", supporter: "SUPPORTER", contributor: "CONTRIBUTOR" },
       validate: true

  has_and_belongs_to_many :user, join_table: "_RoleToUser", foreign_key: "A", association_foreign_key: "B"
end
