defmodule Blinko.Follows do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Blinko.PrismaDateTime, autogenerate: {Blinko.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          site_name: String.t() | nil,
          site_url: String.t(),
          site_avatar: String.t() | nil,
          description: String.t() | nil,
          follow_type: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          account: Blinko.Accounts.t() | nil
        }

  schema "follows" do
    field(:site_name, :string, source: :siteName)
    field(:site_url, :string, source: :siteUrl)
    field(:site_avatar, :string, source: :siteAvatar)
    field(:description, :string)
    field(:follow_type, :string, default: "following", source: :followType)
    field(:account_id, :id, source: :accountId)
    belongs_to(:account, Blinko.Accounts, foreign_key: :account_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end