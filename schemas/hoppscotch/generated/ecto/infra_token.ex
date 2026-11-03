defmodule Hoppscotch.InfraToken do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          creator_uid: String.t(),
          label: String.t(),
          token: String.t(),
          expires_on: DateTime.t() | nil,
          created_on: DateTime.t(),
          updated_on: DateTime.t()
        }

  schema "InfraToken" do
    field(:id, :string, primary_key: true)
    field(:creator_uid, :string, source: :creatorUid)
    field(:label, :string)
    field(:token, :string, autogenerate: {Ecto.UUID, :generate, []})
    field(:expires_on, Hoppscotch.PrismaDateTime, source: :expiresOn)
    field(:created_on, Hoppscotch.PrismaDateTime, autogenerate: true, source: :createdOn)
    field(:updated_on, Hoppscotch.PrismaDateTime, autogenerate: true, source: :updatedOn)
  end
end